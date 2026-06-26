import { cn } from '@/lib/utils'
import { Check } from 'lucide-react'
import { PROJECT_COLORS } from '../../data/data' 

interface ColorPickerProps {
  value: string
  onChange: (color: string) => void
}

export function ColorPicker({ value, onChange }: ColorPickerProps) {
  return (
    <div className='flex flex-wrap gap-2'>
      {PROJECT_COLORS.map((color) => (
        <button
          key={color}
          type='button'
          onClick={() => onChange(color)}
          className={cn(
            'relative h-7 w-7 rounded-full transition-transform hover:scale-110 focus:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2',
            value === color && 'ring-2 ring-ring ring-offset-2'
          )}
          style={{ backgroundColor: color }}
          aria-label={`Select color ${color}`}
        >
          {value === color && (
            <Check
              className='absolute inset-0 m-auto h-3.5 w-3.5 text-white drop-shadow'
              strokeWidth={3}
            />
          )}
        </button>
      ))}
    </div>
  )
}