import Model from "./Model";
import {Association, UserRole} from "./primitives";

export default class User extends Model {
  constructor(data: {id: string, name: string, path: string, [key:string]: any}) {
    super(data);
    this.role = data.role ?? UserRole.Viewer;
    this.associations = data.associations ?? [];
  }

  role: UserRole
  associations: Association[]
}