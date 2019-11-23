defmodule PongBackend.Repo.Migrations.CreatePlayers do
  use Ecto.Migration

  def change do
    create table(:players, primary_key: false) do
      add :id, :uuid, primary_key: true
      add :name, :string
      add :wins, :integer
      add :losses, :integer

      timestamps()
    end

  end
end
