import { PermissionGroupVM } from "./permission-group.vm";

export class PermissionGroupDTO {
    id: string = null!;
    name: string = null!;
    permissions: string[] = [];

    constructor(init?: Partial<PermissionGroupDTO>) {
        Object.assign(this, init);
    }

    public static fromVM(vm: PermissionGroupVM): PermissionGroupDTO {
        return new PermissionGroupDTO({
            id: vm.id,
            name: vm.name,
            permissions: vm.permissions.sort((a, b) => a.name.localeCompare(b.name)).map(e => e.id),
        });
    }
}