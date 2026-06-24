import { useState } from 'react'
import { useAllProjects } from '@/hooks/useQuery/useAllProjects'
import { Header } from '@/components/layout/header'
import { Main } from '@/components/layout/main'
import { ProfileDropdown } from '@/components/profile-dropdown'
import { ThemeSwitch } from '@/components/theme-switch'
import { ProjectTableSkeleton } from './components/project-table-skeleton'
import { ProjectsDialogs } from './components/projects-dialogs'
import { ProjectsEmptyState } from './components/projects-empty-state'
import { ProjectsPrimaryButtons } from './components/projects-primary-buttons'
import { ProjectsProvider } from './components/projects-provider'
import { ProjectsTable } from './components/projects-table'

export function Projects() {
const [page, setPage] = useState(1)
const [pageSize, setPageSize] = useState(10)
const [search, setSearch] = useState('')

const { data, isLoading, isError } = useAllProjects({
  page,
  pageSize,
  search,
})
  const meta = data?.meta
  const projects = (data?.data ?? []).map((project) => ({
    ...project,
    status: project.status.toString(),
  }))

  return (
    <ProjectsProvider>
      <Header fixed>
        <div className='ml-auto flex items-center gap-2'>
          <ThemeSwitch />
          <ProfileDropdown />
        </div>
      </Header>

      <Main className='flex flex-1 flex-col gap-4 sm:gap-6'>
        <div className='flex flex-wrap items-end justify-between gap-2'>
          <div>
            <div className='flex items-center gap-3'>
              <div
                className='h-4 w-4 rounded-md border border-border'
                style={{ backgroundColor: '#FFD500' }}
              />

              <h2 className='text-2xl font-bold tracking-tight'>Projects</h2>
            </div>

            <p className='text-muted-foreground'>
              Here&apos;s a list of your projects!
            </p>
          </div>
          <ProjectsPrimaryButtons />
        </div>
        {isLoading ? (
          <ProjectTableSkeleton />
        ) : isError ? (
          <div className='flex items-center justify-center py-10 text-red-500'>
            Failed to load projects.
          </div>
        ) : projects.length === 0 ? (
          <ProjectsEmptyState />
        ) : (
          <ProjectsTable
            data={projects}
            meta={meta}
            onPageChange={setPage}
            onPageSizeChange={(size) => {
              setPageSize(size)
              setPage(1)
            }}
          />
        )}
      </Main>
      <ProjectsDialogs />
    </ProjectsProvider>
  )
}
