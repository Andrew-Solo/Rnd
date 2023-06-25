import InstanceHeader from "./InstanceHeader";
import {Box} from "@mui/material";
import {observer} from "mobx-react-lite";
import InstanceForm from "./InstanceForm";
import {useState} from "react";
import Form from "../../stores/Form";
import PageLoader from "../ui/PageLoader";
import {useParams} from "react-router-dom";

const InstancePage = observer(({unit}) => {
  const {name} = useParams();
  const [form] = useState(() => new Form(unit, name));

  const {loaded} = form;

  if (!loaded) return (<PageLoader/>);

  return (
    <Box component="main" width={1} display="flex" flexDirection="column">
      <InstanceHeader form={form}/>
      <InstanceForm form={form}/>
    </Box>
  );
});

export default InstancePage;