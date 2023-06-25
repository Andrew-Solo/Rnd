import {Box, SpeedDial, SpeedDialAction, Stack} from "@mui/material";
import {observer} from "mobx-react-lite";
import Field from "./Field";
import {action} from "mobx";
import {Close, ContentCopy, Delete, Done, Edit} from "../icons";

const InstanceForm = observer(({form}) => {
  const editing = form.editing;
  const fields = form.fields.data;

  return (
    <Box padding={4} gap={4} display="flex">
      <Stack spacing={2} width={1} maxWidth={750}>
        {fields
          .filter(field => editing ? !field.readonly : !field.hidden)
          .map(field => <Field key={field.name} form={form} field={field}/>)}
      </Stack>
      <Box height={1} width={56} display="flex" justifyContent="flex-start">
        <SpeedDial onClick={action(() => form.editing = !form.editing)} ariaLabel="Form control" icon={form.editing ? <Done weight={400}/> : <Edit weight={400}/>} direction="down" sx={{ position: 'absolute', top: 170+32, right: 32 }}>
          {form.editing
            ? <SpeedDialAction icon={<Close/>} tooltipTitle="Отмена"/>
            : <SpeedDialAction icon={<ContentCopy/>} tooltipTitle="Дублировать"/>
          }
          <SpeedDialAction icon={<Delete/>} tooltipTitle="Удалить"/>
        </SpeedDial>
      </Box>
    </Box>
  );
});

export default InstanceForm;