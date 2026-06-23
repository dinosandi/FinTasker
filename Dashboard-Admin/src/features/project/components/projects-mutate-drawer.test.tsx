import { useState } from 'react'
import { beforeEach, describe, expect, it, vi } from 'vitest'
import { render } from 'vitest-browser-react'
import { userEvent } from 'vitest/browser'
import { showSubmittedData } from '@/lib/show-submitted-data'
import { type Project } from '../data/schema'
import { ProjectsMutateDrawer } from './projects-mutate-drawer'

vi.mock('@/lib/show-submitted-data', () => ({ showSubmittedData: vi.fn() }))

const MOCK_PROJECT = {
  id: 'project-1',
  name: 'Existing project',
  description: 'Existing project description',
  status: 'in progress',
  color: '#FFD500',
  startDate: '2026-06-01',
  endDate: '2026-06-30',
  createdAt: '2026-06-01T00:00:00Z',
  updatedAt: '2026-06-01T00:00:00Z',
} satisfies Project

describe('ProjectsMutateDrawer', () => {
  beforeEach(() => vi.clearAllMocks())

  it('renders create title and description', async () => {
    const { getByRole, getByText } = await render(
      <ProjectsMutateDrawer open onOpenChange={vi.fn()} />
    )

    const title = getByRole('heading', {
      level: 2,
      name: /Create Project/i,
    })
    const desc = getByText(/Add a new project/i)

    await expect.element(title).toBeInTheDocument()
    await expect.element(desc).toBeInTheDocument()
  })

  it('renders edit title, description, and prefilled title', async () => {
    const { getByRole, getByText } = await render(
      <ProjectsMutateDrawer open onOpenChange={vi.fn()} currentRow={MOCK_PROJECT} />
    )

    const title = getByRole('heading', {
      level: 2,
      name: /Update Project/i,
    })
    const desc = getByText(/Update the project/i)

    const statusSelect = getByRole('combobox', { name: /Status/i })
  

    await expect.element(title).toBeInTheDocument()
    await expect.element(desc).toBeInTheDocument()
    await expect
      .element(statusSelect)
      .toHaveTextContent(new RegExp(MOCK_PROJECT.status, 'i'))
  
  })

  it('shows validation messages when submitting an empty form', async () => {
    const { getByRole, getByText } = await render(
      <ProjectsMutateDrawer open onOpenChange={vi.fn()} />
    )

    const saveButton = getByRole('button', { name: /Save changes/i })
    await userEvent.click(saveButton)

    await expect.element(getByText(/Title is required.$/i)).toBeInTheDocument()
    await expect
      .element(getByText(/Please select a status.$/i))
      .toBeInTheDocument()
    await expect
      .element(getByText(/Please select a label.$/i))
      .toBeInTheDocument()
    await expect
      .element(getByText(/Please choose a priority.$/i))
      .toBeInTheDocument()
  })

  it('submits create form and shows submitted data', async () => {
    const onOpenChange = vi.fn()
    const { getByRole } = await render(
      <ProjectsMutateDrawer open onOpenChange={onOpenChange} />
    )

    const titleInput = getByRole('textbox', { name: /Title/i })
    await userEvent.fill(titleInput, 'New project title')

    const statusSelect = getByRole('combobox', { name: /Status/i })
    await userEvent.click(statusSelect)
    await userEvent.click(getByRole('option', { name: /Todo/i }))

    await userEvent.click(getByRole('radio', { name: /^Bug$/i }))
    await userEvent.click(getByRole('radio', { name: /^Low$/i }))

    const saveButton = getByRole('button', { name: /Save changes/i })
    await userEvent.click(saveButton)

    expect(onOpenChange).toHaveBeenCalledOnce()
    expect(onOpenChange).toHaveBeenCalledWith(false)

    expect(showSubmittedData).toHaveBeenCalledOnce()
    expect(showSubmittedData).toHaveBeenCalledWith({
      name: 'New name',
      status: 'todo',
    })
  })

  it('closes when Close is clicked', async () => {
    const onOpenChange = vi.fn()
    const { getByRole } = await render(
      <ProjectsMutateDrawer open onOpenChange={onOpenChange} />
    )

    const closeButtons = getByRole('dialog')
      .getByRole('button', {
        name: /Close/i,
      })
      .all()
    expect(closeButtons).toHaveLength(2)
    await userEvent.click(closeButtons[1])

    expect(onOpenChange).toHaveBeenCalledOnce()
    expect(onOpenChange).toHaveBeenCalledWith(false)
  })

  it('resets entered values when the sheet is closed and reopened', async () => {
    function Harness() {
      const [open, setOpen] = useState(true)
      return (
        <>
          <button type='button' onClick={() => setOpen(true)}>
            Reopen
          </button>
          <ProjectsMutateDrawer open={open} onOpenChange={setOpen} />
        </>
      )
    }

    const { getByRole } = await render(<Harness />)

    const titleInput = getByRole('textbox', { name: /Title/i })
    await userEvent.fill(titleInput, 'Draft title')
    await expect.element(titleInput).toHaveValue('Draft title')

    const statusSelect = getByRole('combobox', { name: /Status/i })
    await userEvent.click(statusSelect)
    await userEvent.click(getByRole('option', { name: /Todo/i }))
    await expect.element(statusSelect).toHaveTextContent(/Todo/i)

    const labelRadio = getByRole('radio', { name: /^Documentation$/i })
    await userEvent.click(labelRadio)
    await expect.element(labelRadio).toBeChecked()

    const priorityRadio = getByRole('radio', { name: /^High$/i })
    await userEvent.click(priorityRadio)
    await expect.element(priorityRadio).toBeChecked()

    const closeButtons = getByRole('dialog')
      .getByRole('button', {
        name: /Close/i,
      })
      .all()
    await userEvent.click(closeButtons[0])

    const reopenButton = getByRole('button', { name: /Reopen/i })
    await userEvent.click(reopenButton)

    await expect.element(titleInput).toHaveValue('')
    await expect.element(statusSelect).not.toHaveTextContent(/Todo/i)
    await expect.element(labelRadio).not.toBeChecked()
    await expect.element(priorityRadio).not.toBeChecked()
  })
})
