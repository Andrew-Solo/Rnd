import {SvgIcon} from "@mui/material";
import parse from 'html-react-parser';

export default function Icon({icon, ...props}) {
  return (
    <SvgIcon {...props}>
      {parse(icon)}
    </SvgIcon>
  );
}