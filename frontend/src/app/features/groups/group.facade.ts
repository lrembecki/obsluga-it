import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { GroupModel } from './group.model';

export class GroupFacade extends ApiFacade<GroupModel> {
  constructor(endpoint: string) {
    super([], endpoint);
  }
}
