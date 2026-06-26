import {
  Circle,
  CheckCircle,
  Timer,
  CircleOff,
} from 'lucide-react'

export const statuses = [
  {
    label: 'Not Started',
    value: 'NotStarted',
    icon: Circle,
    color: '#ffff',
    bgColor: '#9a9393',
  },
  {
    label: 'In Progress',
    value: 'InProgress',
    icon: Timer,
    color: '#ffff',
    bgColor: '#FFD500',
  },
  {
    label: 'Completed',
    value: 'Completed',
    icon: CheckCircle,
    color: '#ffff',
    bgColor: '#09bd2e',
  },
  {
    label: 'Canceled',
    value: 'Cancelled',
    icon: CircleOff,
    color: '#fff',
    bgColor: '#dd3131',
  },
] as const

export const PROJECT_COLORS = [
  '#0052CC', // Jira Blue
  '#00B8D9', // Cyan
  '#36B37E', // Green
  '#FF5630', // Red
  '#FF8B00', // Orange
  '#6554C0', // Purple
  '#00875A', // Dark Green
  '#403294', // Dark Purple
  '#0065FF', // Bright Blue
  '#DE350B', // Dark Red
  '#FF991F', // Amber
  '#8777D9', // Lavender
]


export const statusField = [
  {
    label: 'Not Started',
    value: '0',
    icon: Circle,
    color: '#9B99F5',
    bgColor: '#9B99F520',
  },
  {
    label: 'In Progress',
    value: '1',
    icon: Timer,
    color: '#FFFF',
    bgColor: '#FFD500',
  },
  {
    label: 'Completed',
    value: '2',
    icon: CheckCircle,
    color: '#FFFF',
    bgColor: '#09bd2e',
  },
  {
    label: 'Canceled',
    value: '3',
    icon: CircleOff,
    color: '#EF4444',
    bgColor: '#EF444420',
  },
] as const