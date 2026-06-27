import { Skeleton } from "@/components/ui/skeleton";

export function TasksTableSkeleton() {
    return (
        <div>
            {Array.from({ length: 8 }).map((_, index) => (
                <div key={index} className="flex items-center gap-4 rounded-lg border p-4">
                    <Skeleton className="h-10 w-10 rounded-full" />
                    <div className="flex-1 space-y-2">
                        <Skeleton className="h-4 w-[250px]" />
                        <Skeleton className="h-4 w-[180px]" />
                    </div>
                    <Skeleton className="h-8 w-20" />
                    <Skeleton className="h-8 w-24" />
                </div>
            ))}
        </div>
    )
}