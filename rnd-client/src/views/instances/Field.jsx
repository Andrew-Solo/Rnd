import StringField from "./text/StringField";
import {Type} from "../../stores/models/primitives";
import {observer} from "mobx-react-lite";

const Field = observer(({form, field}) => {
  switch (field.type) {
    case Type.String: return(<StringField form={form} field={field}/>)
    default: return(<StringField form={form} field={field}/>)
  }
});

export default Field;