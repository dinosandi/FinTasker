import Staytuned from "@/assets/image/Staytuned.svg"

export function MusicEmptyState() {
  return (
    <div className="flex min-h-[70vh] flex-col items-center justify-center px-6 text-center">
      <img
        src={Staytuned}
        alt="Feature Coming Soon"
        className="mb-8 w-72 max-w-full"
      />

      <h2 className="text-2xl font-bold tracking-tight text-foreground">
        Feature Coming Soon
      </h2>

      <p className="mt-3 max-w-md text-sm leading-6 text-muted-foreground">
        This feature is currently under development. Stay tuned for upcoming
        updates.
      </p>

      <div className="mt-6 rounded-full border bg-muted px-4 py-2 text-xs font-medium text-muted-foreground">
        🚧 Under Development
      </div>
    </div>
  )
}