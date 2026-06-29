import { ConfigDrawer } from '@/components/config-drawer'
import { Header } from '@/components/layout/header'
import { Main } from '@/components/layout/main'
import { ProfileDropdown } from '@/components/profile-dropdown'
import { Search } from '@/components/search'
import { ThemeSwitch } from '@/components/theme-switch'
import { TasksDialogs } from './components/tasks-dialogs'
import { TasksPrimaryButtons } from './components/tasks-primary-buttons'
import { TasksProvider } from './components/tasks-provider'
import { TasksTable } from './components/tasks-table'
import { TasksTableSkeleton } from './components/task-table-skeleton'
import { useState } from 'react'
import { useProjectDetailTasks } from '@/hooks/useQuery/useProjectDetailTasks'
import { useParams } from '@tanstack/react-router'
import { ProjectKeyBadge } from '../edit/components/project-key-badge'
import { Badge } from '@/components/ui/badge'
import { LayoutList, ChevronRight } from 'lucide-react'
import { TASK_STATUS } from './data/data'
import { TaskStatus, TaskPriority } from '@/Type'

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

function ProjectDetailEmptyState() {
  return (
    <div className='flex flex-col items-center justify-center py-16 text-center'>
      <div className='mb-3 flex h-12 w-12 items-center justify-center rounded-full bg-muted'>
        <LayoutList size={22} className='text-muted-foreground' />
      </div>
      <p className='text-sm font-medium text-foreground'>No tasks yet</p>
      <p className='mt-1 text-xs text-muted-foreground'>
        Create your first task to get started.
      </p>
    </div>
  )
}

function StatusSummary({ tasks }: { tasks: { status: string }[] }) {
  return (
    <div className='flex items-center gap-2 flex-wrap'>
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
  const { projectId } = useParams({ from: '/_authenticated/projects/$projectId/' })
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
      // API returns numeric enums — map to string values safely
      status: STATUS_MAP[task.status as unknown as number] ?? String(task.status),
      priority: PRIORITY_MAP[task.priority as unknown as number] ?? String(task.priority),
    })) ?? []

  // Derive project info from first task (fallback to projectId slug)
  const projectName = tasks[0]?.projectName ?? 'Project'
  // Derive key: first 2–3 uppercase chars of projectName
  const projectKey = projectName
    .split(/\s+/)
    .map((w) => w[0])
    .join('')
    .toUpperCase()
    .slice(0, 3)

  // Static color derived from projectId to stay consistent
  const PROJECT_COLORS = [
    '#0052CC', '#36B37E', '#FF5630', '#6554C0', '#FF991F',
    '#00B8D9', '#403294', '#DE350B', '#00875A', '#0065FF',
  ]
  const projectColor =
    PROJECT_COLORS[
      projectId.split('').reduce((acc, c) => acc + c.charCodeAt(0), 0) %
        PROJECT_COLORS.length
    ]

  return (
    <TasksProvider>
      <Header fixed>
        <Search className='me-auto' />
        <ThemeSwitch />
        <ConfigDrawer />
        <ProfileDropdown />
      </Header>

      <Main className='flex flex-1 flex-col gap-0'>
        {/* ── Project header (Jira-style) ── */}
        <div className='border-b border-border bg-background px-6 py-4'>
          {/* Breadcrumb */}
          <div className='mb-3 flex items-center gap-1.5 text-xs text-muted-foreground'>
            <span className='hover:text-foreground cursor-pointer transition-colors'>Projects</span>
            <ChevronRight size={12} />
            <span className='font-medium text-foreground'>{projectName}</span>
            <ChevronRight size={12} />
            <span>Board</span>
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
                  <h1 className='text-xl font-bold tracking-tight text-foreground leading-tight'>
                    {projectName}
                  </h1>
                  <Badge
                    variant='secondary'
                    className='h-5 rounded px-1.5 text-[10px] font-bold tracking-wider uppercase'
                    style={{ color: projectColor, backgroundColor: projectColor + '18' }}
                  >
                    {projectKey}
                  </Badge>
                </div>
                <p className='mt-0.5 text-xs text-muted-foreground'>
                  Software project · {meta?.totalCount ?? 0} tasks
                </p>
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
              <h2 className='text-sm font-semibold text-foreground'>Task List</h2>
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
            <ProjectDetailEmptyState />
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