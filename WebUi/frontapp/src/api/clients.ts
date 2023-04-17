import instance from '@/api/instance';
import {UsersClient,} from '../../financeTracker';

export default {
    UsersClient: new UsersClient(undefined, instance),
};

