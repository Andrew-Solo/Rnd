import Unit from "./Unit";

export enum UserRole {
  Viewer = "Viewer",
  Editor = "Editor",
  Moderator = "Moderator",
  Admin = "Admin",
  Owner = "Owner"
}

export interface Association {
  provider: string,
  identifier: string,
  secret: string | null
}

export default class User extends Unit {
  constructor(data: {id: string, name: string, path: string, [key:string]: any}) {
    super(data);
    this.role = data.role ?? UserRole.Viewer;
    this.associations = data.associations ?? [];
  }

  role: UserRole
  associations: Association[]
}