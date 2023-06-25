import {Box, Stack} from "@mui/material";
import {observer} from "mobx-react-lite";
import Field from "./Field";

const InstanceForm = observer(({form}) => {
  const editing = form.editing;
  const fields = form.fields.data;
  fields.forEach(field => console.log(field.name + field.hidden + field.readonly));


  return (
    <Box padding={4}>
      <Stack spacing={2} maxWidth={750}>
        {fields
          .filter(field => editing ? !field.readonly : !field.hidden)
          .map(field => <Field key={field.name} form={form} field={field}/>)}
      </Stack>
    </Box>
  );
});

export default InstanceForm;