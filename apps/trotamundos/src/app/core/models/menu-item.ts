export interface MenuItemModel {
  label: string;
  link?: string;
  routerLink?: string[];
  icon?: string;
  children?: MenuItemModel[];
}
