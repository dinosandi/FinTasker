import { useState } from 'react'
import { useTasksById } from '@/hooks/useQuery/useTasksById'
import { Header } from '@/components/layout/header'
import { Main } from '@/components/layout/main'
import { ProfileDropdown } from '@/components/profile-dropdown'
import { ThemeSwitch } from '@/components/theme-switch'
import { TasksTableSkeleton } from './components/task-table-skeleton'
import { TasksDialogs } from './components/tasks-dialogs'
import { TasksPrimaryButtons } from './components/tasks-primary-buttons'
import { TasksProvider } from './components/tasks-provider'
import { TasksTable } from './components/tasks-table'
import { ProjectsEmptyState } from '../components/projects-empty-state'
import { Route } from '@/routes/_authenticated/projects/$projectId'

export function Tasks() {
    const [page, setPage] = useState(1)
    const [pageSize, setPageSize] = useState(10)
    const { projectId } = Route.useParams()
    
    const { data, isLoading, isError } = useTasksById({
        projectId,
        page,
        pageSize,
        status,
    }) 

    const meta = data?.meta
    const tasks = data?.data?.map(task => ({
        ...task,
        status: String(task.status),
    })) ?? []

    return (
        <TasksProvider>
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
                className='h-8 w-1 rounded-md border border-border'
                style={{ backgroundColor: '#FFD500' }}
              />

              <h2 className='text-2xl font-bold tracking-tight'>Projects</h2>
            </div>

            <p className='text-muted-foreground'>
              Here&apos;s a list of your projects!
            </p>
          </div>
          <TasksPrimaryButtons />
        </div>
        {isLoading ? (
          <TasksTableSkeleton />
        ) : isError ? (
          <div className='flex items-center justify-center py-10 text-red-500'>
            Failed to load projects.
          </div>
        ) : tasks.length === 0 ? (
          <ProjectsEmptyState />
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
      </Main>

            <TasksDialogs />
        </TasksProvider>

    )
}