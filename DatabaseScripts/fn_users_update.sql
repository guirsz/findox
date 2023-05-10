\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_users_update(
	in_user_id integer,
	in_user_name character varying,
	in_email character varying,
	in_role_id integer,
	in_deleted boolean,
	in_updated_date timestamp without time zone,
	in_updated_by integer)
    RETURNS void
    LANGUAGE SQL
AS $BODY$

	UPDATE public.users 
	SET user_name = in_user_name,
		email = in_email,
		role_id = in_role_id,
		deleted = in_deleted,
		updated_date = in_updated_date,
		updated_by = in_updated_by
	WHERE user_id = in_user_id;

$BODY$;

ALTER FUNCTION public.fn_users_update(integer, character varying, character varying, integer, boolean, timestamp without time zone, integer)
    OWNER TO postgres;
