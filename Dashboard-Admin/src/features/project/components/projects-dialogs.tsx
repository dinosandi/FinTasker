import Deleted from '@/assets/image/Deleted.svg'
import { useDeleteProject } from '@/hooks/useMutation/Projects/useDeleteProject'
import { ConfirmDialog } from '@/components/confirm-dialog'
import { ProjectsImportDialog } from './projects-import-dialog'
import { ProjectsMutateDrawer } from './projects-mutate-drawer'
import { useProjects } from './projects-provider'

export function ProjectsDialogs() {
  const DeleteProject = useDeleteProject()
  const { open, setOpen, currentRow, setCurrentRow } = useProjects()
  return (
    <>
      <ProjectsMutateDrawer
        key='project-create'
        open={open === 'create'}
        onOpenChange={() => setOpen('create')}
      />

      <ProjectsImportDialog
        key='projects-import'
        open={open === 'import'}
        onOpenChange={() => setOpen('import')}
      />

      {currentRow && (
        <>
          <ProjectsMutateDrawer
            key={`project-update-${currentRow.id}`}
            open={open === 'update'}
            onOpenChange={() => {
              setOpen('update')
              setTimeout(() => {
                setCurrentRow(null)
              }, 500)
            }}
            currentRow={currentRow}
          />

          <ConfirmDialog
            isLoading={DeleteProject.isPending}
            key='project-delete'
            destructive
            open={open === 'delete'}
            onOpenChange={() => {
              setOpen('delete')
              setTimeout(() => {
                setCurrentRow(null)
              }, 500)
            }}
            handleConfirm={() => {
              DeleteProject.mutate(currentRow.id, {
                onSuccess: () => {
                  setOpen(null)

                  setTimeout(() => {
                    setCurrentRow(null)
                  }, 500)
                },
              })
            }}
            className='max-w-md'
            title={`Delete this project?`}
            desc={
              <div className='flex flex-col items-center gap-4 text-center'>
                <img
                  src={Deleted}
                  alt='Delete Project'
                  className='h-45 w-auto'
                />

                <div>
                  You are about to delete
                  <strong> {currentRow.name} </strong>
                  project.
                  <br />
                  This action cannot be undone.
                </div>
              </div>
            }
            confirmText='Delete'
          />
        </>
      )}
    </>
  )
}
