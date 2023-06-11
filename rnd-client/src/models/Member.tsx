import Model from "./Model";
import User from "./User";
import Game from "./Game";

export default class Member extends Model {
  constructor(data: {name: string, user: User, game: Game, [key:string]: any}) {
    super(data);
    this.user = data.user;
    this.game = data.game;
  }

  user: User
  game: Game
}