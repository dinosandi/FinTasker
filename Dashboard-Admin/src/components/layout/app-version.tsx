import { APP_CONFIG } from '@/config/app'
import {
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
} from '@/components/ui/sidebar'

export function AppVersion() {
  return (
    <SidebarMenu>
      <SidebarMenuItem>
        <SidebarMenuButton
          size="lg"
          className="cursor-default hover:bg-transparent active:bg-transparent"
        >
          <div className="flex h-8 w-8 items-center justify-center rounded-lg bg-primary text-sm font-bold text-primary-foreground">
            F
          </div>

          <div className="grid flex-1 text-left text-sm leading-tight">
            <span className="font-semibold">
              {APP_CONFIG.name}
            </span>

            <span className="text-xs text-muted-foreground">
              v{APP_CONFIG.version}
            </span>
          </div>
        </SidebarMenuButton>
      </SidebarMenuItem>
    </SidebarMenu>
  )
}