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