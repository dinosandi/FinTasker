import { faker } from '@faker-js/faker'

// Set a fixed seed for consistent data generation
faker.seed(12345)

export const projects = Array.from({ length: 100 }, () => {
  const statuses = [
    'todo',
    'in progress',
    'completed',
    'canceled',
    'backlog',
  ] as const

  return {
    id: `PROJECT-${faker.number.int({ min: 1000, max: 9999 })}`,
    name: faker.lorem.sentence({ min: 5, max: 15 }),
    status: faker.helpers.arrayElement(statuses),
    color: faker.color.colorByCSSColorSpace(),
    startDate: faker.date.past(),
    endDate: faker.date.future(),
    createdAt: faker.date.past(),
    updatedAt: faker.date.recent(),
  }
})
