import { ConfigDrawer } from "@/components/config-drawer";
import { Header } from "@/components/layout/header";
import { Main } from "@/components/layout/main";
import { ProfileDropdown } from "@/components/profile-dropdown";
import { Search } from "@/components/search";
import { ThemeSwitch } from "@/components/theme-switch";

import { ProjectsDialogs } from "./components/projects-dialogs";
import { ProjectsPrimaryButtons } from "./components/projects-primary-buttons";
import { ProjectsProvider } from "./components/projects-provider";
import { ProjectsTable } from "./components/projects-table";
import { useAllProjects } from '@/hooks/useQuery/useAllProjects';


export function Projects() {
    const { data, isLoading, isError } = useAllProjects();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError) {
    return <div>Failed to load projects.</div>;
  }


  return (
    <ProjectsProvider>
      <Header fixed>
        <Search className='me-auto' />
        <ThemeSwitch />
        <ConfigDrawer />
        <ProfileDropdown />
      </Header>

      <Main className='flex flex-1 flex-col gap-4 sm:gap-6'>
        <div className='flex flex-wrap items-end justify-between gap-2'>
          <div>
            <h2 className='text-2xl font-bold tracking-tight'>Projects</h2>
            <p className='text-muted-foreground'>
              Here&apos;s a list of your projects for this month!
            </p>
          </div>
          <ProjectsPrimaryButtons />
        </div>
        <ProjectsTable data={(data?.data ?? []).map(project => ({
          ...project,
          status: project.status.toString(),
        }))} />
      </Main>

      <ProjectsDialogs />
    </ProjectsProvider>
  )
}
