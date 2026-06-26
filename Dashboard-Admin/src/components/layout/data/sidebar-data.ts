import {

  LayoutDashboard,
  Monitor,

  ListTodo,

  HelpCircle,

  Bell,

  Palette,

  Settings,
  Wrench,
  UserCog,
  AudioWaveform,
} from 'lucide-react'
import { type SidebarData } from '../types'
import { APP_CONFIG } from '@/config/app'

export const sidebarData: SidebarData = {
  user: {
    name: APP_CONFIG.name,
    email: `Version ${APP_CONFIG.version}`,
    avatar: '/avatars/shadcn.jpg',
  },

  navGroups: [
    {
      title: 'Page',
      items: [
        {
          title: 'Dashboard',
          url: '/',
          icon: LayoutDashboard,
        },
        {
          title: 'Projects',
          url: '/projects',
          icon: Monitor,
        },
        {
          title: 'Finance',
          url: '/finance',
          icon: ListTodo,
        },
        {
          title: 'Music',
          url: '/music',
          icon: AudioWaveform,
        }

      ],
    },
   
    {
      title: 'Other',
      items: [
        {
          title: 'Settings',
          icon: Settings,
          items: [
            {
              title: 'Profile',
              url: '/settings',
              icon: UserCog,
            },
            {
              title: 'Account',
              url: '/settings/account',
              icon: Wrench,
            },
            {
              title: 'Appearance',
              url: '/settings/appearance',
              icon: Palette,
            },
            {
              title: 'Notifications',
              url: '/settings/notifications',
              icon: Bell,
            },
            {
              title: 'Display',
              url: '/settings/display',
              icon: Monitor,
            },
          ],
        },
        {
          title: 'Help Center',
          url: '/help-center',
          icon: HelpCircle,
        },
      ],
    },
  ],
}
