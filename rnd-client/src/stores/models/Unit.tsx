import Model from "./Model";

export default class Unit extends Model {
  readonly moduleId: string

  constructor(data: {id: string, name: string, path: string, moduleId: string, [key:string]: any}) {
    super(data);
    this.moduleId = data.moduleId;

    // makeAutoObservable(this, {
    //   moduleId: false,
    // }, { autoBind: true });
  }
}