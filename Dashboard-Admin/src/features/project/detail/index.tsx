import { useState } from 'react'
import { Link, useParams } from '@tanstack/react-router'
import { TaskStatus, TaskPriority } from '@/Type'
import { LayoutList, ChevronRight } from 'lucide-react'
import { useProjectDetailTasks } from '@/hooks/useQuery/useProjectDetailTasks'
import { Header } from '@/components/layout/header'
import { Main } from '@/components/layout/main'
import { ProfileDropdown } from '@/components/profile-dropdown'
import { ThemeSwitch } from '@/components/theme-switch'
import { ProjectKeyBadge } from '../edit/components/project-key-badge'
import { TasksTableSkeleton } from './components/task-table-skeleton'
import { TasksDialogs } from './components/tasks-dialogs'
import { TasksPrimaryButtons } from './components/tasks-primary-buttons'
import { TasksProvider } from './components/tasks-provider'
import { TasksTable } from './components/tasks-table'
import { TASK_STATUS } from './data/data'
import { TasksEmptyState } from './components/tasks-empty-state'

// Map numeric enum → string value used by TASK_STATUS / TASK_PRIORITY
const STATUS_MAP: Record<number, string> = {
  [TaskStatus.ToDo]: 'ToDo',
  [TaskStatus.InProgress]: 'InProgress',
  [TaskStatus.Review]: 'Review',
  [TaskStatus.Completed]: 'Completed',
  [TaskStatus.Cancelled]: 'Cancelled',
}

const PRIORITY_MAP: Record<number, string> = {
  [TaskPriority.Low]: 'Low',
  [TaskPriority.Medium]: 'Medium',
  [TaskPriority.High]: 'High',
  [TaskPriority.Critical]: 'Critical',
}

function StatusSummary({ tasks }: { tasks: { status: string }[] }) {
  return (
    <div className='flex flex-wrap items-center gap-2'>
      {TASK_STATUS.map((s) => {
        const count = tasks.filter((t) => t.status === s.value).length
        if (count === 0) return null
        const Icon = s.icon
        return (
          <span
            key={s.value}
            className='inline-flex items-center gap-1 rounded-full px-2 py-0.5 text-xs font-medium'
            style={{ color: s.color, backgroundColor: s.bgColor }}
          >
            <Icon size={11} strokeWidth={2.5} />
            {count} {s.label}
          </span>
        )
      })}
    </div>
  )
}

export function Tasks() {
  const { projectId } = useParams({
    from: '/_authenticated/projects/$projectId/',
  })
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(10)
  const [search] = useState('')

  const { data, isLoading, isError } = useProjectDetailTasks({
    projectId,
    page,
    pageSize,
    search,
  })

  const meta = data?.meta
  const tasks =
    data?.data.map((task) => ({
      ...task,
      status:
        STATUS_MAP[task.status as unknown as number] ?? String(task.status),
      priority:
        PRIORITY_MAP[task.priority as unknown as number] ??
        String(task.priority),
    })) ?? []

  const projectName = tasks[0]?.projectName ?? 'Project'
  const projectKey = projectName
    .split(/\s+/)
    .map((w) => w[0])
    .join('')
    .toUpperCase()
    .slice(0, 3)

  const PROJECT_COLORS = [
    '#0052CC',
    '#36B37E',
    '#FF5630',
    '#6554C0',
    '#FF991F',
    '#00B8D9',
    '#403294',
    '#DE350B',
    '#00875A',
    '#0065FF',
  ]
  const projectColor =
    PROJECT_COLORS[
      projectId.split('').reduce((acc, c) => acc + c.charCodeAt(0), 0) %
        PROJECT_COLORS.length
    ]

  return (
    <TasksProvider>
      <Header fixed>
      <div className='ml-auto flex items-center gap-2'>
          <ThemeSwitch />
          <ProfileDropdown />
        </div>
      </Header>

      <Main className='flex flex-1 flex-col gap-0'>
        {/* ── Project header (Jira-style) ── */}
        <div className='border-b border-border bg-background px-6 py-4'>
          {/* Breadcrumb */}
          <div className='mb-3 flex items-center gap-1.5 text-xs text-muted-foreground'>
            <span className='cursor-pointer transition-colors hover:text-foreground'>
              <Link 
              to='/projects'
              className='hover:text-gray-900 hover:underline'
              >
              Projects
              </Link>
              
            </span>
            <ChevronRight size={12} />
            <span>Tasks</span>
            <ChevronRight size={12} />
            <span className='font-medium text-foreground'>{projectName}</span>
          </div>

          {/* Project identity row */}
          <div className='flex flex-wrap items-start justify-between gap-4'>
            <div className='flex items-center gap-3'>
              <ProjectKeyBadge
                projectKey={projectKey}
                color={projectColor}
                className='h-10 w-10 rounded-lg text-sm shadow-md'
              />
              <div>
                <div className='flex items-center gap-2'>
                  <h1 className='text-xl leading-tight font-bold tracking-tight text-foreground'>
                    {projectName}
                  </h1>

                </div>

              </div>
            </div>

            <TasksPrimaryButtons />
          </div>

          {/* Status summary chips */}
          {tasks.length > 0 && (
            <div className='mt-3'>
              <StatusSummary tasks={tasks} />
            </div>
          )}
        </div>

        {/* ── Tasks content ── */}
        <div className='flex flex-1 flex-col gap-4 px-6 py-5'>
          <div className='flex items-center justify-between'>
            <div className='flex items-center gap-2'>
              <LayoutList size={15} className='text-muted-foreground' />
              <h2 className='text-sm font-semibold text-foreground'>
                Task List
              </h2>
              {meta && (
                <span className='rounded bg-muted px-1.5 py-0.5 text-xs font-medium text-muted-foreground'>
                  {meta.totalCount}
                </span>
              )}
            </div>
          </div>

          {isLoading ? (
            <TasksTableSkeleton />
          ) : isError ? (
            <div className='flex items-center justify-center rounded-lg border border-destructive/30 bg-destructive/5 py-10 text-sm text-destructive'>
              Failed to load tasks. Please try again.
            </div>
          ) : tasks.length === 0 ? (
            <TasksEmptyState/>
          ) : (
            <TasksTable
              data={tasks}
              meta={meta}
              onPageChange={setPage}
              onPageSizeChange={(size) => {
                setPageSize(size)
                setPage(1)
              }}
            />
          )}
        </div>
      </Main>

      <TasksDialogs />
    </TasksProvider>
  )
}
