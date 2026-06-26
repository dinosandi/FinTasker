import { z } from 'zod'
import { format } from 'date-fns'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { CalendarIcon } from 'lucide-react'
import { cn } from '@/lib/utils'
import { usePostProject } from '@/hooks/useMutation/Projects/usePostProject'
import { Button } from '@/components/ui/button'
import { Calendar } from '@/components/ui/calendar'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogClose,
} from '@/components/ui/dialog'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from '@/components/ui/popover'
import { Textarea } from '@/components/ui/textarea'
import { SelectDropdown } from '@/components/select-dropdown'
import { statusField } from '../data/data'
import { type Project } from '../data/schema'
import { toast } from 'sonner'

type ProjectMutateDrawerProps = {
  open: boolean
  onOpenChange: (open: boolean) => void
  currentRow?: Project
}


const formSchema = z.object({
  name: z.string().min(1, 'Project name is required.'),
  status: z.string().min(1, 'Please select a status.'),
  description: z.string().min(1, 'Description is required.'),
  color: z.string().min(1, 'Please select a color.'),
  startDate: z.string().min(1, 'Please select a start date.'),
  endDate: z.string().min(1, 'Please select an end date.'),
})

type ProjectForm = z.infer<typeof formSchema>

export function ProjectsMutateDrawer({
  open,
  onOpenChange,
  currentRow,
}: ProjectMutateDrawerProps) {
  const isUpdate = !!currentRow
  const { mutate: postProject, isPending } = usePostProject()

  const form = useForm<ProjectForm>({
    resolver: zodResolver(formSchema),
    defaultValues: currentRow ?? {
      name: '',
      status: '',
      description: '',
      color: '',
      startDate: '',
      endDate: '',
    },
  })

  const onSubmit = (data: ProjectForm) => {
    const payload = {
      name: data.name,
      description: data.description,
      status: parseInt(data.status, 10),
      color: data.color,
      startDate: new Date(data.startDate),
      endDate: new Date(data.endDate),
    }
  
    postProject(payload, {
      onSuccess: () => {
        onOpenChange(false)
        form.reset()
      },
  
      onError: (error) => {
        toast.error(error?.response?.data?.errors)
      },
    })
  }

  return (
    <Dialog
      open={open}
      onOpenChange={(v) => {
        onOpenChange(v)
        if (!v) form.reset()
      }}
    >
      <DialogContent className='gap-6 sm:max-w-[520px]'>
        <DialogHeader className='text-start'>
          <DialogTitle>{isUpdate ? 'Update' : 'Create'} Project</DialogTitle>
          <DialogDescription>
            {isUpdate
              ? 'Update the project by providing necessary info.'
              : 'Add a new project by providing necessary info.'}{' '}
            Click save when you&apos;re done.
          </DialogDescription>
        </DialogHeader>

        <Form {...form}>
          <form
            id='projects-form'
            onSubmit={form.handleSubmit(onSubmit)}
            className='space-y-4'
          >
            <FormField
              control={form.control}
              name='name'
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Project Name</FormLabel>
                  <FormControl>
                    <Input
                      {...field}
                      placeholder='Enter project name'
                      disabled={isPending}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <div className='grid grid-cols-2 gap-4'>
              <FormField
                control={form.control}
                name='status'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Status</FormLabel>
                    <SelectDropdown
                      defaultValue={field.value}
                      onValueChange={field.onChange}
                      placeholder='Select status'
                      disabled={isPending}
                      items={statusField.map((status) => ({
                        label: status.label,
                        value: status.value,
                      }))}
                    />
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name='color'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Color Tag</FormLabel>
                    <FormControl>
                      <div className='flex items-center gap-2'>
                        <div className='relative h-10 w-10 flex-shrink-0 cursor-pointer overflow-hidden rounded-md border focus-within:ring-2 focus-within:ring-ring'>
                          <input
                            type='color'
                            value={field.value || '#000000'}
                            onChange={(e) => field.onChange(e.target.value)}
                            disabled={isPending}
                            className='absolute inset-0 h-[200%] w-[200%] -translate-x-1/4 -translate-y-1/4 cursor-pointer'
                          />
                        </div>

                        <Input
                          type='text'
                          placeholder='#ffd500'
                          value={field.value}
                          onChange={(e) => field.onChange(e.target.value)}
                          disabled={isPending}
                          maxLength={7}
                          className='font-mono uppercase'
                        />
                      </div>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className='grid grid-cols-2 gap-4'>
              <FormField
                control={form.control}
                name='startDate'
                render={({ field }) => (
                  <FormItem className='flex flex-col'>
                    <FormLabel className='mb-1'>Start Date</FormLabel>
                    <Popover>
                      <PopoverTrigger asChild>
                        <FormControl>
                          <Button
                            variant={'outline'}
                            className={cn(
                              'w-full pl-3 text-left font-normal',
                              !field.value && 'text-muted-foreground'
                            )}
                            disabled={isPending}
                          >
                            {field.value ? (
                              format(new Date(field.value), 'PPP')
                            ) : (
                              <span>Pick a date</span>
                            )}
                            <CalendarIcon className='ml-auto h-4 w-4 opacity-50' />
                          </Button>
                        </FormControl>
                      </PopoverTrigger>
                      <PopoverContent className='w-auto p-0' align='start'>
                        <Calendar
                          mode='single'
                          selected={
                            field.value ? new Date(field.value) : undefined
                          }
                          onSelect={(date: Date | undefined) =>
                            field.onChange(date?.toISOString())
                          }
                          disabled={() => isPending}
                          initialFocus
                        />
                      </PopoverContent>
                    </Popover>
                    <FormMessage />
                  </FormItem>
                )}
              />

              <FormField
                control={form.control}
                name='endDate'
                render={({ field }) => (
                  <FormItem className='flex flex-col'>
                    <FormLabel className='mb-1'>End Date</FormLabel>
                    <Popover>
                      <PopoverTrigger asChild>
                        <FormControl>
                          <Button
                            variant={'outline'}
                            className={cn(
                              'w-full pl-3 text-left font-normal',
                              !field.value && 'text-muted-foreground'
                            )}
                            disabled={isPending}
                          >
                            {field.value ? (
                              format(new Date(field.value), 'PPP')
                            ) : (
                              <span>Pick a date</span>
                            )}
                            <CalendarIcon className='ml-auto h-4 w-4 opacity-50' />
                          </Button>
                        </FormControl>
                      </PopoverTrigger>
                      <PopoverContent className='w-auto p-0' align='start'>
                        <Calendar
                          mode='single'
                          selected={
                            field.value ? new Date(field.value) : undefined
                          }
                          onSelect={(date: Date | undefined) =>
                            field.onChange(date?.toISOString())
                          }
                          disabled={() => isPending}
                          initialFocus
                        />
                      </PopoverContent>
                    </Popover>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <FormField
              control={form.control}
              name='description'
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Description</FormLabel>
                  <FormControl>
                    <Textarea
                      {...field}
                      placeholder='Enter project description...'
                      className='resize-y'
                      disabled={isPending}
                      maxLength={350}
                    />
                  </FormControl>
                  <div className="text-sm text-muted-foreground text-right">
        {(field.value?.length ?? 0)}/350
      </div>
      
                  <FormMessage />
                </FormItem>
              )}
            />
          </form>
        </Form>

        <DialogFooter className='gap-2 sm:gap-2'>
          <DialogClose asChild>
            <Button variant='outline' disabled={isPending}>
              Close
            </Button>
          </DialogClose>
          <Button
            form='projects-form'
            type='submit'
            disabled={isPending}
            className='bg-[#FFD500] text-black hover:bg-[#FFD500]/90 disabled:bg-[#FFD500]/50 disabled:text-black/50'
          >
            {isPending ? 'Saving...' : 'Save changes'}
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
