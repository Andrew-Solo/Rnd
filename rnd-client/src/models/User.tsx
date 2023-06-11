export enum UserRole {
  Viewer = "Viewer",
  Editor = "Editor",
  Admin = "Admin",
}

export default class User {
  constructor(data: any) {
    this.id = data.id ?? "";
    this.email = data.email ?? "";
    this.login = data.login ?? "";
    this.role = data.role ?? UserRole.Viewer;
    this.image = data.image;
    this.discordId = data.discordId;
    this.registered = data.registered ?? new Date();
    this.games = data.games ?? [];
  }

  id: string
  email: string
  login: string
  role: UserRole
  image: string | null
  discordId: number | null
  registered: Date
  games: any[]
}