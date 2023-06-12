import {Box, Button, Stack} from "@mui/material";
import {AccountCircle, Lock, Mail} from "../icons";
import IconTextField from "./IconTextField";
import Form from "../../models/Form";
import {observer} from "mobx-react-lite";

const Register = observer((props: {form: Form}) => {
  const {createInputProps} = props.form;

  return (
    <Stack width={1} gap={2}>
      {/*TODO увеличить бордер, сделать по ярче*/}
      <IconTextField {...createInputProps("login")} placeholder="Логин" icon={<AccountCircle/>}/>
      <IconTextField {...createInputProps("email")} placeholder="Почта" icon={<Mail/>}/>
      <IconTextField {...createInputProps("password")} placeholder="Пароль" type="password" icon={<Lock/>}/>
      <IconTextField {...createInputProps("confirmPassword")} placeholder="Повтор пароля" type="password" icon={<Lock/>}/>
      <Box gap={4} display="flex">
        <Button fullWidth variant="contained" color="secondary" href="/account/login">Вход</Button>
        <Button fullWidth variant="contained">Регистрация</Button>
      </Box>
    </Stack>
  );
});
export default Register