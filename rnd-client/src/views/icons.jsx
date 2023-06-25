import {SvgIcon} from "@mui/material";
import { ReactComponent as HomeSvg } from "../assets/home.svg";
import { ReactComponent as GroupSvg } from "../assets/group.svg";
import { ReactComponent as HistorySvg } from "../assets/history.svg";
import { ReactComponent as AddSvg } from "../assets/add.svg";
import { ReactComponent as FilterListSvg } from "../assets/filterList.svg";
import { ReactComponent as FilterList400Svg } from "../assets/filterList400.svg";
import { ReactComponent as AccountCircleSvg } from "../assets/accountCircle.svg";
import { ReactComponent as LockSvg } from "../assets/lock.svg";
import { ReactComponent as MailSvg } from "../assets/mail.svg";
import { ReactComponent as DoneSvg } from "../assets/done.svg";
import { ReactComponent as Done400Svg } from "../assets/done400.svg";
import { ReactComponent as ExpandMoreSvg } from "../assets/expandMore.svg";
import { ReactComponent as ExpandLessSvg } from "../assets/expandLess.svg";
import { ReactComponent as EditSvg } from "../assets/edit.svg";
import { ReactComponent as Edit400Svg } from "../assets/edit400.svg";
import { ReactComponent as DeleteSvg } from "../assets/delete.svg";
import { ReactComponent as CloseSvg } from "../assets/close.svg";
import { ReactComponent as ContentCopySvg } from "../assets/contentCopy.svg";

export function Home(props) {
  return (
    <SvgIcon {...props}>
      <HomeSvg/>
    </SvgIcon>
  );
}

export function Group(props) {
  return (
    <SvgIcon {...props}>
      <GroupSvg/>
    </SvgIcon>
  );
}

export function History(props) {
  return (
    <SvgIcon {...props}>
      <HistorySvg/>
    </SvgIcon>
  );
}

export function Add(props) {
  return (
    <SvgIcon {...props}>
      <AddSvg/>
    </SvgIcon>
  );
}

export function FilterList({weight = 300, ...props}) {

  const svg = weight === 400
    ? <FilterList400Svg/>
    : <FilterListSvg/>

  return (
    <SvgIcon {...props}>
      {svg}
    </SvgIcon>
  );
}

export function AccountCircle(props) {
  return (
    <SvgIcon {...props}>
      <AccountCircleSvg/>
    </SvgIcon>
  );
}

export function Lock(props) {
  return (
    <SvgIcon {...props}>
      <LockSvg/>
    </SvgIcon>
  );
}

export function Mail(props) {
  return (
    <SvgIcon {...props}>
      <MailSvg/>
    </SvgIcon>
  );
}

export function Done({weight = 300, ...props}) {
  const svg = weight === 400
    ? <Done400Svg/>
    : <DoneSvg/>

  return (
    <SvgIcon {...props}>
      {svg}
    </SvgIcon>
  );
}


export function ExpandMore(props) {
  return (
    <SvgIcon {...props}>
      <ExpandMoreSvg/>
    </SvgIcon>
  );
}

export function ExpandLess(props) {
  return (
    <SvgIcon {...props}>
      <ExpandLessSvg/>
    </SvgIcon>
  );
}

export function Edit({weight = 300, ...props}) {
  const svg = weight === 400
    ? <Edit400Svg/>
    : <EditSvg/>

  return (
    <SvgIcon {...props}>
      {svg}
    </SvgIcon>
  );
}

export function Delete(props) {
  return (
    <SvgIcon {...props}>
      <DeleteSvg/>
    </SvgIcon>
  );
}

export function Close(props) {
  return (
    <SvgIcon {...props}>
      <CloseSvg/>
    </SvgIcon>
  );
}

export function ContentCopy(props) {
  return (
    <SvgIcon {...props}>
      <ContentCopySvg/>
    </SvgIcon>
  );
}
