import { useNavigate } from '@tanstack/react-router'
import { Link } from '@tanstack/react-router'
import { toast } from 'sonner'
import {
  usePutProject,
  type UpdateProjectRequest,
} from '@/hooks/useMutation/Projects/usePutProject'
import { useProjectDetail } from '@/hooks/useQuery/useProjectDetail'
import { Header } from '@/components/layout/header'
import { Main } from '@/components/layout/main'
import { ProfileDropdown } from '@/components/profile-dropdown'
import { ThemeSwitch } from '@/components/theme-switch'
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
          },
        })
        navigate({ to: '/projects' })
      },
      onError: () => {
        toast.error('Failed to update project', {
          style: {
            background: '#e52a2a',
            color: '#fff',
          },
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

        <div className='sticky top-0 z-10 mb-6 border-b border-gray-200 bg-white px-4 py-3'>
          {/* Breadcrumbs */}
          <div className='flex items-center gap-2 text-xs text-gray-500'>
            <Link
              to='/projects'
              className='hover:text-gray-900 hover:underline'
            >
              Projects
            </Link>

            <span>/</span>

            <span className='text-gray-400'>Edit</span>

            {project?.name && (
              <>
                <span>/</span>
                <span className='font-medium text-gray-700'>
                  {project.name}
                </span>
              </>
            )}
          </div>

          {/* Title Row */}
          <div className='mt-2 flex items-center gap-3'>
            <div
              className='h-8 w-1 rounded-md border border-border'
              style={{ backgroundColor: project?.color ?? '#0052CC' }}
            />

            <div>
              <h2 className='text-lg font-bold text-gray-900'>
                Update Project
              </h2>
              <p className='mt-0.5 text-sm text-gray-500'>
                Manage project settings and configuration
              </p>
            </div>
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
