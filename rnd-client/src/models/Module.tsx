import Unit from "./Unit";


export default class Module extends Unit {
  // eslint-disable-next-line @typescript-eslint/no-useless-constructor
  constructor(data: {id: string, name: string, path: string, [key:string]: any}) {
    super(data);
  }
}