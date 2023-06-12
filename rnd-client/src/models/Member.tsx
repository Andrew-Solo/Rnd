import Unit from "./Unit";
import User from "./User";
import Game from "./Game";

export default class Member extends Unit {
  constructor(data: {id: string, name: string, path: string, user: User, game: Game, [key:string]: any}) {
    super(data);
    this.user = data.user;
    this.game = data.game;
  }

  user: User
  game: Game
}