export interface UserDto {
  id: string;
  email: string;
  display_name: string;
  role: string;
  is_active: boolean;
}

export interface UsersListDto {
  items: UserDto[];
  total: number;
}
