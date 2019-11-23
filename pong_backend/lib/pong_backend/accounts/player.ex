defmodule PongBackend.Accounts.Player do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key {:id, :binary_id, autogenerate: true}

  schema "players" do
    field :losses, :integer
    field :name, :string
    field :wins, :integer

    timestamps()
  end

  @doc false
  def changeset(player, attrs) do
    player
    |> cast(attrs, [:name, :wins, :losses])
    |> validate_required([:name, :wins, :losses])
  end

  def registration_changeset(struct, params \\ %{}) do
    struct
    |> cast(params, [:name])
    |> validate_required([:name])
  end
end
