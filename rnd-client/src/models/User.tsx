import Unit from "./Unit";
import Game from "./Game";

export enum UserRole {
  Viewer = "Viewer",
  Editor = "Editor",
  Admin = "Admin",
}

export default class User extends Unit {
  constructor(data: {id: string, name: string, path: string, email: string, [key:string]: any}) {
    super(data);
    this.games = data.games ?? [];
    this.email = data.email;
    this.role = data.role ?? UserRole.Viewer;
    this.discordId = data.discordId ?? null;
  }

  // Logic
  games: Game[]
  email: string
  role: UserRole
  discordId: number | null
}