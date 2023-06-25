import {Box, Stack} from "@mui/material";
import {observer} from "mobx-react-lite";

const InstanceForm = observer(({form}) => {
  const {fields, editing} = form;

  return (
    <Box padding={4}>
      <Stack spacing={2} maxWidth={750}>
        {/*{fields.data*/}
        {/*  .filter(field => editing ? field.editable : field.visible)*/}
        {/*  .map(field => <Field key={field.name} editing={editing} value={data[field.name]} {...field}/>)}*/}
      </Stack>
    </Box>
  );
});

export default InstanceForm;