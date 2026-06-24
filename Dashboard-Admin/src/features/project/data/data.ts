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
    color: '#000000',
    bgColor: '#9B99F520',
  },
  {
    label: 'In Progress',
    value: 'InProgress',
    icon: Timer,
    color: '#000000',
    bgColor: '#FFD500',
  },
  {
    label: 'Completed',
    value: 'Completed',
    icon: CheckCircle,
    color: '#000000',
    bgColor: '#09bd2e',
  },
  {
    label: 'Canceled',
    value: 'Cancelled',
    icon: CircleOff,
    color: '#000000',
    bgColor: '#dd3131',
  },
] as const

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