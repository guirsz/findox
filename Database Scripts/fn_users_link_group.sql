CREATE OR REPLACE FUNCTION public.fn_users_link_group(
	in_user_id integer,
	in_group_id integer,
	in_grouped_date timestamp without time zone,
	in_grouped_by integer)
    RETURNS boolean
    LANGUAGE SQL
AS $BODY$

	INSERT INTO public.user_groups(user_id, group_id, grouped_date, grouped_by)
	VALUES (in_user_id, in_group_id, in_grouped_date, in_grouped_by)
	RETURNING TRUE;

$BODY$;

ALTER FUNCTION public.fn_users_link_group(integer, integer, timestamp without time zone, integer)
    OWNER TO postgres;
