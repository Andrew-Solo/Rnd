import Model from "./Model";
import {Association, Role} from "./primitives";

export default class User extends Model {
  role: Role
  associations: Association[]

  constructor(data: {id: string, name: string, path: string, [key:string]: any}) {
    super(data);
    this.role = data.role ?? Role.Viewer;
    this.associations = data.associations ?? [];
  }
}