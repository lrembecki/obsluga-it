import { User, UserRole } from './user.model';
import { UserDto } from './user.dto';

function toRole(raw: string): UserRole {
  if (raw === 'ADMIN' || raw === 'USER' || raw === 'VIEWER') return raw;
  return 'VIEWER';
}

export function mapUserDtoToModel(dto: UserDto): User {
  return {
    id: dto.id,
    email: dto.email,
    displayName: dto.display_name,
    role: toRole(dto.role),
    isActive: dto.is_active,
  };
}
