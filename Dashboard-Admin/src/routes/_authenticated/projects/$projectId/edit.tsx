import { createFileRoute } from '@tanstack/react-router'
import { EditProject } from '@/features/project/edit'

export const Route = createFileRoute(
  '/_authenticated/projects/$projectId/edit',
)({
  component: RouteComponent,
})

function RouteComponent() {
  const { projectId } = Route.useParams()
  return <EditProject projectId={projectId} />
}