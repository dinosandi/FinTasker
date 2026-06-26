import Nodata from "@/assets/image/Nodata.svg"

export function ProjectsEmptyState() {
  return (
    <div className="flex flex-col items-center justify-center py-16">
      <img
        src={Nodata}
        alt="No Data"
        className="mb-6 w-72 max-w-full"
      />

      <h3 className="text-xl font-semibold">
        No Projects Found
      </h3>

      <p className="mt-2 text-muted-foreground">
        Create your first project to start managing tasks.
      </p>
    </div>
  )
}