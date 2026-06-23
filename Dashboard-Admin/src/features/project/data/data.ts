import {
  Circle,
  CheckCircle,
  Timer,
  CircleOff,
} from 'lucide-react'


export const statuses = [
  
  {
    label: 'Not Started',
    value: 'todo' as const,
    icon: Circle,
  },
  {
    label: 'In Progress',
    value: 'in progress' as const,
    icon: Timer,
  },
  
  {
    label: 'Completed',
    value: 'completed' as const,
    icon: CheckCircle,
  },
  {
    label: 'Canceled',
    value: 'canceled' as const,
    icon: CircleOff,
  },
]

