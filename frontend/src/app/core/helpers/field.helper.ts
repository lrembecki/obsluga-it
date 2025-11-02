export function fieldValue<T>(record: T, name: string): unknown {
  return (name || '').split('.').reduce((acc: any, item: string) => {
    return acc ? acc[item] : null!;
  }, record);
}
