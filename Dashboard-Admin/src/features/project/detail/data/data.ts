import {
  Circle,
  Clock,
  Eye,
  CheckCircle2,
  XCircle,
  ArrowDown,
  Minus,
  ArrowUp,
  Zap,
} from 'lucide-react'

export const TASK_STATUS = [
  {
    label: 'To Do',
    value: 'ToDo',
    icon: Circle,
    color: '#64748B',
    bgColor: '#F1F5F9',
  },
  {
    label: 'In Progress',
    value: 'InProgress',
    icon: Clock,
    color: '#2563EB',
    bgColor: '#DBEAFE',
  },
  {
    label: 'In Review',
    value: 'Review',
    icon: Eye,
    color: '#F59E0B',
    bgColor: '#FEF3C7',
  },
  {
    label: 'Completed',
    value: 'Completed',
    icon: CheckCircle2,
    color: '#16A34A',
    bgColor: '#DCFCE7',
  },
  {
    label: 'Cancelled',
    value: 'Cancelled',
    icon: XCircle,
    color: '#DC2626',
    bgColor: '#FEE2E2',
  },
] as const

export const TASK_PRIORITY = [
  {
    label: 'Low',
    value: 'Low',
    icon: ArrowDown,
    color: '#16A34A',
    bgColor: '#DCFCE7',
  },
  {
    label: 'Medium',
    value: 'Medium',
    icon: Minus,
    color: '#64748B',
    bgColor: '#F1F5F9',
  },
  {
    label: 'High',
    value: 'High',
    icon: ArrowUp,
    color: '#F59E0B',
    bgColor: '#FEF3C7',
  },
  {
    label: 'Critical',
    value: 'Critical',
    icon: Zap,
    color: '#DC2626',
    bgColor: '#FEE2E2',
  },
] as const