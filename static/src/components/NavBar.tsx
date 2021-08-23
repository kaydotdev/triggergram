import {
    AppBar,
    Toolbar
} from '@material-ui/core';
import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';

import NotificationButton from './NotificationButton';
import MenuBanner from './MenuBanner';
import ProfileMenu from './ProfileMenu';


const useStyles = makeStyles((_: Theme) =>
    createStyles({
        appbar: {
            color: '#fff',
            backgroundColor: '#000'
        },
        grow: {
            flexGrow: 1
        }
    }),
);

function NavBar() {
    const classes = useStyles();

    return (
        <div className={classes.grow}>
            <AppBar className={classes.appbar} position="static">
                <Toolbar>
                    <MenuBanner />
                    <div className={classes.grow} />
                    <NotificationButton notificationCount={0} />
                    <ProfileMenu />
                </Toolbar>
            </AppBar>
        </div>
    );
}

export default NavBar;
