import { useNavigate } from '@tanstack/react-router'
import { toast } from 'sonner'

import { Header } from '@/components/layout/header'
import { Main } from '@/components/layout/main'
import { ProfileDropdown } from '@/components/profile-dropdown'
import { ThemeSwitch } from '@/components/theme-switch'

import { useProjectDetail } from '@/hooks/useQuery/useProjectDetail'
import { usePutProject, type UpdateProjectRequest } from '@/hooks/useMutation/Projects/usePutProject'

import { EditProjectForm } from './components/project-edit-form'
import { EditProjectSkeleton } from './components/project-edit-skeleton'

interface EditProjectProps {
  projectId: string
}

export function EditProject({ projectId }: EditProjectProps) {
  const navigate = useNavigate()

  const { data: project, isLoading, isError } = useProjectDetail(projectId)
  const { mutate: updateProject, isPending } = usePutProject()

  function handleSubmit(data: UpdateProjectRequest) {
    updateProject(data, {
      onSuccess: () => {
        toast.success('Project updated', {
          style: {
            background: '#22C55E',
            color: '#fff',
          }
        })
        navigate({ to: '/projects'})
      },
      onError: () => {
        toast.error('Failed to update project', {
          style: {
            background: '#e52a2a',
            color: '#fff',
          }
        })
      },
    })
  }

  function handleCancel() {
    navigate({ to: '/projects/$projectId', params: { projectId } })
  }

  return (
    <>
      <Header fixed>
        <div className='ml-auto flex items-center gap-2'>
          <ThemeSwitch />
          <ProfileDropdown />
        </div>
      </Header>

      <Main>
        {/* Page header */}
        <div className='mb-6 flex flex-wrap items-end justify-between gap-2'>
          <div>
            <div className='flex items-center gap-3'>
              <div
                className='h-4 w-4 rounded-md border border-border'
                style={{ backgroundColor: project?.color ?? '#0052CC' }}
              />
              <h2 className='text-2xl font-bold tracking-tight'>
                Project Settings
              </h2>
            </div>
            <p className='text-muted-foreground'>
              {project?.name
                ? `Update details for "${project.name}"`
                : 'Update project details and settings.'}
            </p>
          </div>
        </div>

        {/* Content */}
        {isError ? (
          <div className='flex items-center justify-center py-10 text-red-500'>
            Failed to load project.
          </div>
        ) : isLoading || !project ? (
          <EditProjectSkeleton />
        ) : (
          <EditProjectForm
            project={project}
            onSubmit={handleSubmit}
            isPending={isPending}
            onCancel={handleCancel}
          />
        )}
      </Main>
    </>
  )
}