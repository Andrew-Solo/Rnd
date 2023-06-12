import Model from "./Model";
import Member from "./Member";
import User from "./User";

export default class Game extends Model {
  constructor(data: {name: string, path: string, owner: User, [key:string]: any}) {
    super(data);
    this.owner = data.owner;
    this.members = data.members ?? [];
    this.archived = data.archived ?? new Date();
  }

  owner: User
  members: Member[]
  archived: Date | null
}