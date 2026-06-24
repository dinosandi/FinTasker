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
    color: '#9B99F5',
    bgColor: '#9B99F520',
  },
  {
    label: 'In Progress',
    value: 'InProgress',
    icon: Timer,
    color: '#FFFF',
    bgColor: '#FFD500',
  },
  {
    label: 'Completed',
    value: 'Completed',
    icon: CheckCircle,
    color: '#FFFF',
    bgColor: '#09bd2e',
  },
  {
    label: 'Canceled',
    value: 'Cancelled',
    icon: CircleOff,
    color: '#EF4444',
    bgColor: '#EF444420',
  },
] as const