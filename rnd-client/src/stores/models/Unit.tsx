import Model from "./Model";

export default class Unit extends Model {
  readonly moduleId: string
  default: boolean
  hidden: boolean
  order: number

  constructor(data: {id: string, name: string, path: string, moduleId: string, [key:string]: any}) {
    super(data);
    this.moduleId = data.moduleId;
    this.default = data.default ?? false;
    this.hidden = data.hidden ?? false;
    this.order = data.order ?? 8;

    // makeAutoObservable(this, {
    //   moduleId: false,
    // }, { autoBind: true });
  }
}