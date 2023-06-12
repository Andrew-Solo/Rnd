import Unit from "./Unit";


export default class Module extends Unit {
  constructor(data: {id: string, name: string, path: string, [key:string]: any}) {
    super(data);
    this.default = data.default ?? false;
    this.hidden = data.hidden ?? false;
  }

  default: boolean
  hidden: boolean
}