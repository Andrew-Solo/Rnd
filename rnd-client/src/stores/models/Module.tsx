import Model from "./Model";

export default class Module extends Model {
  version: string
  readonly creatorId: string
  mainId: string | null
  system: boolean
  default: boolean
  hidden: boolean

  constructor(data: {id: string, name: string, path: string, creatorId: string, [key:string]: any}) {
    super(data);
    this.version = data.version ?? "0.1.0";
    this.creatorId = data.creatorId;
    this.mainId = data.mainId ?? null;
    this.system = data.system ?? false;
    this.default = data.default ?? false;
    this.hidden = data.hidden ?? false;

    // makeAutoObservable(this, {
    //   creatorId: false,
    // }, { autoBind: true });
  }
}