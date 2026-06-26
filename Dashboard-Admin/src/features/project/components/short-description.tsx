import { useState } from 'react'

function DescriptionCell({ description }: { description: string }) {
  const [expanded, setExpanded] = useState(false)

  return (
    <div className='max-w-[400px]'>
      <p
        className={
          expanded
            ? 'text-sm text-muted-foreground whitespace-pre-wrap'
            : 'text-sm text-muted-foreground line-clamp-2'
        }
      >
        {description}
      </p>

      {description.length > 100 && (
        <button
          type='button'
          onClick={() => setExpanded(!expanded)}
          className='mt-1 text-xs font-medium text-primary hover:underline'
        >
          {expanded ? 'Show less' : 'Show more'}
        </button>
      )}
    </div>
  )
}

export {
  DescriptionCell
} 