import Notasks from "@/assets/image/Notasks.svg"

export function TasksEmptyState() {
  return (
    <div className="flex flex-col items-center justify-center py-16">
      <img
        src={Notasks}
        alt="No Data"
        className="mb-6 w-72 max-w-full"
      />

      <h3 className="text-xl font-semibold">
        No tasks yet
      </h3>

      <p className="mt-2 text-muted-foreground">
        Create your first project to start managing tasks.
      </p>
    </div>
  )
}