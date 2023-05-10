\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_groups_update(
	in_group_id integer,
	in_group_name character varying,
	in_deleted boolean,
	in_updated_date timestamp without time zone,
	in_updated_by integer)
    RETURNS void
    LANGUAGE SQL
AS $BODY$

	UPDATE public.groups 
	SET group_name = in_group_name,
		deleted = in_deleted,
		updated_date = in_updated_date,
		updated_by = in_updated_by
	WHERE group_id = in_group_id;

$BODY$;

ALTER FUNCTION public.fn_groups_update(integer, character varying, boolean, timestamp without time zone, integer)
    OWNER TO postgres;
